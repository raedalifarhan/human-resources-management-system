'use client'

import React, { useEffect, useState } from 'react'
import Input from '../Input'
import { FieldValues, useForm } from 'react-hook-form';
import { usePathname, useRouter } from 'next/navigation';
import { Button } from 'flowbite-react';
import { UserFormValues } from '@/types/userTypes';
import SelectInput from '../SelectInput';
import agent from '@/app/api/agent';
import toast from 'react-hot-toast';

interface SelectOptions {
  label: string
  value: string
}

function RegisterForm() {

  const router = useRouter();
  const pathname = usePathname();
  const [options, setOptions] = useState<SelectOptions[]>([]);
  const [responseError, setResponseError] = useState([]);

  const {
    control, handleSubmit, setFocus, formState: { isSubmitting, isValid, isDirty, errors }
  } = useForm({
    mode: 'onTouched'
  });

  useEffect(() => {
    const selectOptions: SelectOptions[] = [];
    agent.Branches.list().then(res => {
      res.map(branch => {
        selectOptions.push({ label: branch.branchName, value: branch.id })
      })
    }).then(() => {
      setOptions(selectOptions);
    })
    setFocus('displayName');
  }, [setFocus])

  const onSubmit = async (data: FieldValues) => {

    try {
      await agent.Account.register(data);
      toast.success('تم اضافة مستخدم جديد بنجاح');
      // redirect
      router.push(`/`);

    } catch (error: any) {
      if (error?.response?.data) {
        
        setResponseError(error?.response?.data)

        toast.error(`لم يتم اضافة المستخدم`);
      } else {
        // If no specific error message is available, display a generic error
        toast.error('حدث خطأ أثناء إرسال البيانات');
        console.error('Error submitting form:', error);
      }
    }

  }

  return (
    <>
    <ul className='my-5 p-5 bg-red-100 text-red-700 border-red-800 rounded-md'>
    {responseError && responseError.map((error: any, i) => (
      <li key={i}>{error?.description}</li>
    ))}
    </ul>
    <form className='flex flex-col gap-3' onSubmit={handleSubmit(onSubmit)}>

      <Input label='الاسم الظاهر' name='displayName'
        control={control}
        rules={{ required: 'الاسم الظاهر حقل مطلوب' }} />

      <Input label='اسم المستخدم' name='username'
        control={control}
        rules={{ required: 'اسم المستخدم حقل مطلوب' }} />

      <Input label='البريد الالكبروني' name='email'
        control={control}
        rules={{ required: 'البريد الالكتروني مطلوب' }} />

      <Input label='كلمة المرور' name='password'
        control={control}
        rules={{ required: 'كلمة المرور حقل مطلوب' }} />

      <SelectInput
        label='الفرع'
        name='branchId'
        control={control}
        rules={{ required: 'اختيار فرع ضروري' }}
        options={options}
      />


      <Button
        isProcessing={isSubmitting}
        disabled={!isValid}
        type='submit'
        outline color='success' >تسجيل</Button>

    </form>
    </>
  )
}

export default RegisterForm