"use client"
import Headings from '@/components/company/Headings'
import React, { useEffect, useState } from 'react'
import LicenceForm from '../../LicenceForm'
import agent from '@/app/api/agent';
import { useRouter } from 'next/navigation';
import toast from 'react-hot-toast';

function UpdateLicence({ params }: { params: { companyId: string } }) {

    const router = useRouter()
    const [licence, setLicence] = useState<Licence>();

    useEffect(() => {
        const res = agent.Licences.lastLicence(params.companyId).then(res => {
            setLicence(res)
        }).catch(error => {
            toast.error(`لا يوجد طلب ترخيص لتعديله`)
            router.push(`/companies/details/${params.companyId}`)
        })
    }, [params.companyId])

    if (!licence) return <p>Loading...</p>


    return (
        <div className='mx-auto max-w-[75%] rounded-lg shadow-lg p-10 bg-white'>
            <Headings
                title='تقديم طلب ترخيص'
                subtitle='ادخل تفاصيل طلب الترخيص بالكامل او قيمة افراضية.'
            />

            <LicenceForm
                licence={licence}
                companyId={params.companyId}
            />
        </div>
    )
}

export default UpdateLicence