import {
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import Quill from "@/components/global/molecules/QuillEditor"
import TimeAllocationTable from "@/components/global/molecules/TimeAllocation"

interface OtherTabProps {
  form: any
  timeAllocation: any
}

const OtherTab = ({ form, timeAllocation }: OtherTabProps) => {
  return (
    <div>
      <div className="flex flex-wrap lg:flex-nowrap w-full mb-8 gap-4">
        <div>
          <TimeAllocationTable
            title="Time allocation"
            horizontal={true}
            items={{
              values: Object.values(timeAllocation),
              labels: Object.keys(timeAllocation)
            }}
            state="default"
          />
        </div>

        <div className="w-full rounded-xl shadow-lg bg-white">
          <div className="flex items-center justify-center rounded-t-lg bg-primary text-white font-medium py-2">
            Assessment Schema
          </div>
          <div className="p-6">
            <div className="grid grid-cols-2 gap-6">
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.quizCount"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label htmlFor="quizCount" className="font-semibold">
                        Quiz Count
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="quizCount"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.quizPercent"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label htmlFor="quizPercent" className="font-semibold">
                        Quiz Percent (%)
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="quizPercent"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.assignmentCount"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label
                        htmlFor="assignmentCount"
                        className="font-semibold"
                      >
                        Assignment Count
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="assignmentCount"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.assignmentPercent"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label
                        htmlFor="assignmentPercent"
                        className="font-semibold"
                      >
                        Assignment Percent (%)
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="assignmentPercent"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.finalTheoryPercent"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label
                        htmlFor="finalTheoryPercent"
                        className="font-semibold"
                      >
                        Final Theory Percent (%)
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="finalTheoryPercent"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="flex flex-col">
                <FormField
                  control={form.control}
                  name="assessment.finalPracticePercent"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <label
                        htmlFor="finalPracticePercent"
                        className="font-semibold"
                      >
                        Final Practice Percent (%)
                      </label>
                      <FormControl>
                        <input
                          type="number"
                          id="finalPracticePercent"
                          className="rounded-md border-gray-300 border p-2 focus:outline-none focus:border-primary"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
            </div>
            {form.formState.errors.assessment?.root?.message && (
              <p className="text-red-500 mt-4">
                {form.formState.errors.assessment?.root.message}
              </p>
            )}
          </div>
        </div>
      </div>

      <FormField
        control={form.control}
        name="trainingPrinciple"
        render={({ field }) => (
          <FormItem className="flex w-full flex-col">
            <FormControl>
              <div className="w-full">
                <Quill title="Training delivery principle" field={field} />
              </div>
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}

export default OtherTab
